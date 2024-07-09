// License: Apache 2.0. See LICENSE file in root directory.
// Copyright(c) 2020 FRAMOS GmbH.
#if FRAMOS
using System;
using System.Collections;
using System.Collections.Generic;
using Intel.RealSense.Base;

namespace Intel.RealSense
{
    public static class D400e
    {
        public struct PortRange
        {
            public ushort min;
            public ushort max;

            public PortRange(ushort min, ushort max)
            {
                this.min = min;
                this.max = max;
            }
        }

        public enum PortType
        {
            All = 0,
            Control = 1,
            Stream = 2,
            Message = 3
        }

        public class PortList : Base.Object, IEnumerable<ushort>
        {
            public PortList(IntPtr ptr, Deleter deleter) : base(ptr, deleter)
            {
            }

            public PortList(IntPtr ptr) 
                : base(ptr, NativeMethods.rs2_d400e_delete_port_list)
            {
            }

            public IEnumerator<ushort> GetEnumerator()
            {
                object error;

                uint portCount = NativeMethods.rs2_d400e_get_port_count(Handle, out error);
                for (uint i = 0; i < portCount; i++)
                {
                    var port = NativeMethods.rs2_d400e_get_port(Handle, i, out error);
                    yield return port;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public uint Count
            {
                get
                {
                    object error;
                    uint portCount = NativeMethods.rs2_d400e_get_port_count(Handle, out error);
                    return portCount;
                }
            }

            public ushort this[uint index]
            {
                get
                {
                    object error;
                    var port = NativeMethods.rs2_d400e_get_port(Handle, index, out error);
                    return port;
                }

            }
        }

        public static double GetHeartbeatTime()
        {
            object err;
            return NativeMethods.rs2_d400e_get_heartbeat_time(out err);
        }

        public static void SetHeartbeatTime(double time)
        {
            object err;
            NativeMethods.rs2_d400e_set_heartbeat_time(time, out err);
        }

        public static int GetBufferCount()
        {
            object err;
            return NativeMethods.rs2_d400e_get_buffer_count(out err);
        }

        public static void SetBufferCount(int bufferCount)
        {
            object err;
            NativeMethods.rs2_d400e_set_buffer_count(bufferCount, out err);
        }

        public static int ToggleDeviceDiagnostics(string device_serial, int toggle)
        {
            object err;
            return NativeMethods.rs2_d400e_toggle_device_diagnostics(device_serial, toggle, out err);
        }

        public static void SetPortRange(PortRange range)
        {
            object err;
            NativeMethods.rs2_d400e_set_port_range(range.min, range.max, out err);
        }

        public static PortRange GetPortRange()
        {
            object err;
            PortRange range;
            NativeMethods.rs2_d400e_get_port_range(out range.min, out range.max, out err);
            return range;
        }

        public static PortList QueryPorts(PortType type = PortType.All)
        {
            object err;
            var ptr = NativeMethods.rs2_d400e_query_ports_by_type(type, out err);
            return new PortList(ptr);
        }
    }
}
#endif
