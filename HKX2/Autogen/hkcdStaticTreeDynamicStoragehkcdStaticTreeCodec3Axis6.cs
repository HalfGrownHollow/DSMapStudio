using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public class hkcdStaticTreeDynamicStoragehkcdStaticTreeCodec3Axis6 : IHavokObject
    {
        public List<hkcdStaticTreeCodec3Axis6> m_nodes;
        
        public virtual void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            m_nodes = des.ReadClassArray<hkcdStaticTreeCodec3Axis6>(br);
        }
        
        public virtual void Write(BinaryWriterEx bw)
        {
        }
    }
}