﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Service.RedisServices
{
    public class RedisService
    {
        private readonly string _host;
        private readonly int _port;
        private ConnectionMultiplexer _ConnectionMultiplexer;
        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect() => _ConnectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

        public IDatabase GetDb(int db = 1)
        {
            return _ConnectionMultiplexer.GetDatabase(db);
        }
    }
}
