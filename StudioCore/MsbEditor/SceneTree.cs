﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using ImGuiNET;
using Veldrid;
using System.Windows.Forms;

namespace StudioCore.MsbEditor
{

    public struct DragDropPayload
    {
        public Entity Entity;
    }

    public struct DragDropPayloadReference
    {
        public int Index;
    }

    public interface SceneTreeEventHandler
    {
        public void OnEntityContextMenu(Entity ent);
    }

    public class SceneTree
    {
        private Universe _universe;
        private ActionManager _editorActionManager;
        private Gui.Viewport _viewport;
        private AssetLocator _assetLocator;
        private Selection _selection;

        private string _id;

        private SceneTreeEventHandler _handler;

        private string _chaliceMapID = "m29_";
        private bool _chaliceLoadError = false;

        private bool _GCNeedsCollection = false;

        private Dictionary<string, Dictionary<MapEntity.MapEntityType, Dictionary<Type, List<MapEntity>>>> _cachedTypeView = null;

        private Dictionary<int, DragDropPayload> _dragDropPayloads = new Dictionary<int, DragDropPayload>();
        private int _dragDropPayloadCounter = 0;

        private List<Entity> _dragDropSources = new List<Entity>();
        private List<int> _dragDropDests = new List<int>();
        private List<Entity> _dragDropDestObjects = new List<Entity>();

        private bool _setNextFocus = false;

        public enum ViewMode
        {
            Hierarchy,
            Flat,
            ObjectType,
        }

        private string[] _viewModeStrings =
        {
            "Hierarchy View",
            "Flat View",
            "Type View",
        };

        private ViewMode _viewMode = ViewMode.Flat;

        public SceneTree(SceneTreeEventHandler handler, string id, Universe universe, Selection sel, ActionManager aman, Gui.Viewport vp, AssetLocator al)
        {
            _handler = handler;
            _id = id;
            _universe = universe;
            _selection = sel;
            _editorActionManager = aman;
            _viewport = vp;
            _assetLocator = al;
        }

        private void RebuildTypeViewCache(Map map)
        {
            if (_cachedTypeView == null)
            {
                _cachedTypeView = new Dictionary<string, Dictionary<MapEntity.MapEntityType, Dictionary<Type, List<MapEntity>>>>();
            }

            var mapcache = new Dictionary<MapEntity.MapEntityType, Dictionary<Type, List<MapEntity>>>();
            mapcache.Add(MapEntity.MapEntityType.Part, new Dictionary<Type, List<MapEntity>>());
            mapcache.Add(MapEntity.MapEntityType.Region, new Dictionary<Type, List<MapEntity>>());
            mapcache.Add(MapEntity.MapEntityType.Event, new Dictionary<Type, List<MapEntity>>());
            if (_assetLocator.Type == GameType.DarkSoulsIISOTFS)
            {
                mapcache.Add(MapEntity.MapEntityType.DS2Event, new Dictionary<Type, List<MapEntity>>());
                mapcache.Add(MapEntity.MapEntityType.DS2EventLocation, new Dictionary<Type, List<MapEntity>>());
                mapcache.Add(MapEntity.MapEntityType.DS2Generator, new Dictionary<Type, List<MapEntity>>());
                mapcache.Add(MapEntity.MapEntityType.DS2GeneratorRegist, new Dictionary<Type, List<MapEntity>>());
            }

            foreach (var obj in map.Objects)
            {
                if (obj is MapEntity e && mapcache.ContainsKey(e.Type))
                {
                    var typ = e.WrappedObject.GetType();
                    if (!mapcache[e.Type].ContainsKey(typ))
                    {
                        mapcache[e.Type].Add(typ, new List<MapEntity>());
                    }
                    mapcache[e.Type][typ].Add(e);
                }
            }

            if (!_cachedTypeView.ContainsKey(map.Name))
            {
                _cachedTypeView.Add(map.Name, mapcache);
            }
            else
            {
                _cachedTypeView[map.Name] = mapcache;
            }
        }

        private void ChaliceDungeonImportButton()
        {
            ImGui.Selectable($@"   {ForkAwesome.PlusCircle} Load Chalice Dungeon...", false);
            if (ImGui.BeginPopupContextItem("chalice", 0))
            {
                ImGui.AlignTextToFramePadding();
                ImGui.Text("Chalice ID (m29_xx_xx_xx): ");
                ImGui.SameLine();
                var pname = _chaliceMapID;
                ImGui.SetNextItemWidth(100);
                if (_chaliceLoadError)
                {
                    ImGui.PushStyleColor(ImGuiCol.FrameBg, new Vector4(0.8f, 0.2f, 0.2f, 1.0f));
                }
                if (ImGui.InputText("##chalicename", ref pname, 12))
                {
                    _chaliceMapID = pname;
                }
                if (_chaliceLoadError)
                {
                    ImGui.PopStyleColor();
                }
                ImGui.SameLine();
                if (ImGui.Button("Load"))
                {
                    if (!_universe.LoadMap(_chaliceMapID))
                    {
                        _chaliceLoadError = true;
                    }
                    else
                    {
                        ImGui.CloseCurrentPopup();
                        _chaliceLoadError = false;
                        _chaliceMapID = "m29_";
                    }
                }
                ImGui.EndPopup();
            }
        }

        unsafe private void MapObjectSelectable(Entity e, bool visicon, bool hierarchial=false)
        {
            // Main selectable
            if (e is MapEntity me)
            {
                ImGui.PushID(me.Type.ToString() + e.Name);
            }
            else
            {
                ImGui.PushID(e.Name);
            }
            bool doSelect = false;
            if (_setNextFocus)
            {
                ImGui.SetItemDefaultFocus();
                _setNextFocus = false;
                doSelect = true;
            }
            bool nodeopen = false;
            string padding = hierarchial ? "   " : "    ";
            if (hierarchial && e.Children.Count > 0)
            {
                var treeflags = ImGuiTreeNodeFlags.OpenOnArrow | ImGuiTreeNodeFlags.SpanAvailWidth;
                if ( _selection.GetSelection().Contains(e))
                {
                    treeflags |= ImGuiTreeNodeFlags.Selected;
                }
                nodeopen = ImGui.TreeNodeEx(e.PrettyName, treeflags);
                if (ImGui.IsItemHovered() && ImGui.IsMouseDoubleClicked(0))
                {
                    if (e.RenderSceneMesh != null)
                    {
                        _viewport.FrameBox(e.RenderSceneMesh.GetBounds());
                    }
                }
            }
            else
            {
                if (ImGui.Selectable(padding + e.PrettyName, _selection.GetSelection().Contains(e), ImGuiSelectableFlags.AllowDoubleClick | ImGuiSelectableFlags.AllowItemOverlap))
                {
                    // If double clicked frame the selection in the viewport
                    if (ImGui.IsMouseDoubleClicked(0))
                    {
                        if (e.RenderSceneMesh != null)
                        {
                            _viewport.FrameBox(e.RenderSceneMesh.GetBounds());
                        }
                    }
                }
            }
            if (ImGui.IsItemClicked(0))
            {
                doSelect = true;
            }

            if (ImGui.IsItemFocused() && !_selection.IsSelected(e))
            {
                doSelect = true;
            }

            if (ImGui.BeginPopupContextItem())
            {
                _handler.OnEntityContextMenu(e);
                ImGui.EndPopup();
            }

            if (ImGui.BeginDragDropSource())
            {
                ImGui.Text(e.PrettyName);
                // Kinda meme
                DragDropPayload p = new DragDropPayload();
                p.Entity = e;
                _dragDropPayloads.Add(_dragDropPayloadCounter, p);
                DragDropPayloadReference r = new DragDropPayloadReference();
                r.Index = _dragDropPayloadCounter;
                _dragDropPayloadCounter++;
                GCHandle handle = GCHandle.Alloc(r, GCHandleType.Pinned);
                ImGui.SetDragDropPayload("entity", handle.AddrOfPinnedObject(), (uint)sizeof(DragDropPayloadReference));
                ImGui.EndDragDropSource();
                handle.Free();
            }
            if (hierarchial && ImGui.BeginDragDropTarget())
            {
                var payload = ImGui.AcceptDragDropPayload("entity");
                if (payload.NativePtr != null)
                {
                    DragDropPayloadReference* h = (DragDropPayloadReference*)payload.Data;
                    var pload = _dragDropPayloads[h->Index];
                    _dragDropPayloads.Remove(h->Index);
                    _dragDropSources.Add(pload.Entity);
                    _dragDropDestObjects.Add(e);
                    _dragDropDests.Add(e.Children.Count);
                }
                ImGui.EndDragDropTarget();
            }

            // Visibility icon
            if (visicon)
            {
                ImGui.SetItemAllowOverlap();
                bool visible = e.EditorVisible;
                ImGui.SameLine(ImGui.GetWindowContentRegionWidth() - 18.0f);
                ImGui.PushStyleColor(ImGuiCol.Text, visible ? new Vector4(1.0f, 1.0f, 1.0f, 1.0f)
                    : new Vector4(0.6f, 0.6f, 0.6f, 1.0f));
                ImGui.TextWrapped(visible ? ForkAwesome.Eye : ForkAwesome.EyeSlash);
                ImGui.PopStyleColor();
                if (ImGui.IsItemClicked(0))
                {
                    e.EditorVisible = !e.EditorVisible;
                    doSelect = false;
                }
            }

            // If the visibility icon wasn't clicked actually perform the selection
            if (doSelect)
            {
                if (InputTracker.GetKey(Key.ControlLeft) || InputTracker.GetKey(Key.ControlRight))
                {
                    _selection.AddSelection(e);
                }
                else
                {
                    _selection.ClearSelection();
                    _selection.AddSelection(e);
                }
            }

            ImGui.PopID();

            // Invisible item to be a drag drop target between nodes
            if (e is MapEntity me2)
            {
                ImGui.SetItemAllowOverlap();
                ImGui.InvisibleButton(me2.Type.ToString() + e.Name, new Vector2(-1, 4.0f));
            }
            else
            {
                ImGui.SetItemAllowOverlap();
                ImGui.InvisibleButton(e.Name, new Vector2(-1, 4.0f));
            }
            if (ImGui.IsItemFocused())
            {
                _setNextFocus = true;
            }
            if (ImGui.BeginDragDropTarget())
            {
                var payload = ImGui.AcceptDragDropPayload("entity");
                if (payload.NativePtr != null)
                {
                    DragDropPayloadReference* h = (DragDropPayloadReference*)payload.Data;
                    var pload = _dragDropPayloads[h->Index];
                    _dragDropPayloads.Remove(h->Index);
                    if (hierarchial)
                    {
                        _dragDropSources.Add(pload.Entity);
                        _dragDropDestObjects.Add(e.Parent);
                        _dragDropDests.Add(e.Parent.ChildIndex(e) + 1);
                    }
                    else
                    {
                        _dragDropSources.Add(pload.Entity);
                        _dragDropDests.Add(pload.Entity.Container.Objects.IndexOf(e) + 1);
                    }
                    
                }
                ImGui.EndDragDropTarget();
            }

            // If there's children then draw them
            if (nodeopen)
            {
                HierarchyView(e);
                ImGui.TreePop();
            }
        }

        private void HierarchyView(Entity entity)
        {
            foreach (var obj in entity.Children)
            {
                if (obj is Entity e)
                {
                    MapObjectSelectable(e, true, true);
                }
            }
        }

        private void FlatView(Map map)
        {
            foreach (var obj in map.Objects)
            {
                if (obj is MapEntity e)
                {
                    MapObjectSelectable(e, true);
                }
            }
        }

        private void TypeView(Map map)
        {
            if (_cachedTypeView == null || !_cachedTypeView.ContainsKey(map.Name))
            {
                RebuildTypeViewCache(map);
            }

            foreach (var cats in _cachedTypeView[map.Name].OrderBy(q => q.Key.ToString()))
            {
                if (cats.Value.Count > 0)
                {
                    if (ImGui.TreeNodeEx(cats.Key.ToString(), ImGuiTreeNodeFlags.OpenOnArrow))
                    {
                        foreach (var typ in cats.Value.OrderBy(q => q.Key.Name))
                        {
                            if (typ.Value.Count > 0)
                            {
                                // Regions don't have multiple types in games before DS3
                                if (cats.Key == MapEntity.MapEntityType.Region &&
                                    _assetLocator.Type != GameType.DarkSoulsIII && _assetLocator.Type != GameType.Sekiro)
                                {
                                    foreach (var obj in typ.Value)
                                    {
                                        MapObjectSelectable(obj, true);
                                    }
                                }
                                else if (ImGui.TreeNodeEx(typ.Key.Name, ImGuiTreeNodeFlags.OpenOnArrow))
                                {
                                    foreach (var obj in typ.Value)
                                    {
                                        MapObjectSelectable(obj, true);
                                    }
                                    ImGui.TreePop();
                                }
                            }
                            else
                            {
                                ImGui.Text($@"   {typ.Key.ToString()}");
                            }
                        }
                        ImGui.TreePop();
                    }
                }
                else
                {
                    ImGui.Text($@"   {cats.Key.ToString()}");
                }

            }
        }

        public void OnGui()
        {
            ImGui.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0.145f, 0.145f, 0.149f, 1.0f));
            ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0.0f, 0.0f));
            if (ImGui.Begin($@"Map Object List##{_id}"))
            {
                ImGui.PopStyleVar();
                int mode = (int)_viewMode;
                ImGui.SetNextItemWidth(-1);
                if (ImGui.Combo("##typecombo", ref mode, _viewModeStrings, _viewModeStrings.Length))
                {
                    _viewMode = (ViewMode)mode;
                }

                ImGui.BeginChild("listtree");
                Map pendingUnload = null;
                foreach (var lm in _universe.LoadedObjectContainers.OrderBy((k) => k.Key))
                {
                    var map = lm.Value;
                    var mapid = lm.Key;
                    var treeflags = ImGuiTreeNodeFlags.OpenOnArrow | ImGuiTreeNodeFlags.SpanAvailWidth;
                    if (map != null && _selection.GetSelection().Contains(map.RootObject))
                    {
                        treeflags |= ImGuiTreeNodeFlags.Selected;
                    }
                    bool nodeopen = false;
                    if (map != null)
                    {
                        nodeopen = ImGui.TreeNodeEx($@"{ForkAwesome.Cube} {mapid}", treeflags);
                    }
                    else
                    {
                        ImGui.Selectable($@"   {ForkAwesome.Cube} {mapid}", false);
                    }
                    // Right click context menu
                    if (ImGui.BeginPopupContextItem($@"mapcontext_{mapid}"))
                    {
                        if (map == null)
                        {
                            if (ImGui.Selectable("Load Map"))
                            {
                                _universe.LoadMap(mapid);
                            }
                        }
                        else if (map is Map m)
                        {
                            if (ImGui.Selectable("Save Map"))
                            {
                                try
                                {
                                    _universe.SaveMap(m);
                                }
                                catch (SavingFailedException e)
                                {
                                    System.Windows.Forms.MessageBox.Show(e.Wrapped.Message, e.Message,
                                         System.Windows.Forms.MessageBoxButtons.OK,
                                         System.Windows.Forms.MessageBoxIcon.None);
                                }
                            }
                            if (ImGui.Selectable("Unload Map"))
                            {
                                _selection.ClearSelection();
                                _editorActionManager.Clear();
                                pendingUnload = m;
                            }
                        }
                        ImGui.EndPopup();
                    }
                    if (ImGui.IsItemClicked() && map != null)
                    {
                        if (InputTracker.GetKey(Key.ShiftLeft) || InputTracker.GetKey(Key.ShiftRight))
                        {
                            _selection.AddSelection(map.RootObject);
                        }
                        else
                        {
                            _selection.ClearSelection();
                            _selection.AddSelection(map.RootObject);
                        }
                    }
                    if (nodeopen)
                    {
                        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(8.0f, 0.0f));
                        if (_viewMode == ViewMode.Hierarchy)
                        {
                            HierarchyView(map.RootObject);
                        }
                        else if (_viewMode == ViewMode.Flat)
                        {
                            FlatView((Map)map);
                        }
                        else if (_viewMode == ViewMode.ObjectType)
                        {
                            TypeView((Map)map);
                        }
                        ImGui.PopStyleVar();
                        ImGui.TreePop();
                    }
                }
                if (_assetLocator.Type == GameType.Bloodborne)
                {
                    ChaliceDungeonImportButton();
                }
                ImGui.EndChild();

                if (_dragDropSources.Count > 0)
                {
                    if (_dragDropDestObjects.Count > 0)
                    {
                        var action = new ChangeEntityHierarchyAction(_universe, _dragDropSources, _dragDropDestObjects, _dragDropDests, false);
                        _editorActionManager.ExecuteAction(action);
                        _dragDropSources.Clear();
                        _dragDropDests.Clear();
                        _dragDropDestObjects.Clear();
                    }
                    else
                    {
                        var action = new ReorderContainerObjectsAction(_universe, _dragDropSources, _dragDropDests, false);
                        _editorActionManager.ExecuteAction(action);
                        _dragDropSources.Clear();
                        _dragDropDests.Clear();
                    }
                }

                if (pendingUnload != null)
                {
                    _universe.UnloadMap(pendingUnload);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    _GCNeedsCollection = true;
                    Resource.ResourceManager.UnloadUnusedResources();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
            else
            {
                ImGui.PopStyleVar();
            }
            ImGui.End();
            ImGui.PopStyleColor();
        }
    }
}