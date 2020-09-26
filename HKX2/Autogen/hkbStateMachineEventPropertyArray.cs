using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public class hkbStateMachineEventPropertyArray : hkReferencedObject
    {
        public List<hkbEventProperty> m_events;
        
        public override void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            base.Read(des, br);
            m_events = des.ReadClassArray<hkbEventProperty>(br);
        }
        
        public override void Write(BinaryWriterEx bw)
        {
            base.Write(bw);
        }
    }
}