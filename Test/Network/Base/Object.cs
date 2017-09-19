﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using XSNetwork.Interface;

namespace XSNetwork.Base
{
    enum ASYNC_TYPE
    {
        ASYNC_ACCEPT    = 0,
        ASYNC_CONNECT,
        ASYNC_CLOSE,
        ASYNC_RECV,
        ASYNC_SEND,
        ASYNC_MAX
    };

    public class Object : Logger, IObject
    {
        protected Socket m_Socket;
        protected IPEndPoint m_LocalIPEndPoint;
        protected IPEndPoint m_RemoteIPEndPoint;

        public String LocalAddress { get { return m_LocalIPEndPoint == null ? "" : m_LocalIPEndPoint.Address.ToString(); } }
        public Int32 LocalPort { get { return m_LocalIPEndPoint == null ? 0 : m_LocalIPEndPoint.Port; } }
        public IPEndPoint LocalIPEndPoint { get { return m_LocalIPEndPoint; } }

        public String RemoteAddress { get { return m_RemoteIPEndPoint == null ? "" : m_RemoteIPEndPoint.Address.ToString(); } }
        public Int32 RemotePort { get { return m_RemoteIPEndPoint == null ? 0 : m_RemoteIPEndPoint.Port; } }
        public IPEndPoint RemoteIPEndPoint { get { return m_RemoteIPEndPoint; } }

        protected bool m_IsListening;
        public bool IsListening { get { return m_IsListening; } }
        protected bool m_IsConnecting;
        public bool IsConnecting { get { return m_IsConnecting; } }

        public Object()
        {
            m_IsListening = false;
            m_IsConnecting = false;
        }

        public virtual void dispose()
        {
            // 关闭套接字
            if (m_Socket != null)
            {
                if (IsConnecting || IsListening)
                { this.close(); }

                m_Socket.Dispose();
                m_Socket = null;
            }

            m_LocalIPEndPoint = null;
            m_RemoteIPEndPoint = null;
        }

        public virtual bool initialize()
        {
            return true;
        }

        public virtual void close()
        {
            if (this.IsConnecting || this.IsListening)
            {
                m_Socket.Close();

                m_IsListening = false;
                m_IsConnecting = false;
            }
        }
    }
}
