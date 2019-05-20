using NLog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace searchfight.presentation.Common
{

    public class Logger
    {
        private static NLog.Logger _logger;

        public static NLog.Logger Log
        {
            get
            {
                if (_logger == null)
                {
                    _logger = LogManager.GetCurrentClassLogger();
                }


                return _logger;
            }
        }

        protected static void Info(HttpResponseMessage response)
        {
            if (response != null)
            {
                _logger.Info(response.ToString());
            }
        }

        protected static void Trace(string message)
        {
            _logger.Trace(message);
        }

        protected static void Error(Exception ex)
        {
            if (ex != null)
            {
                _logger.Error(ex);
            }
        }
    }

}
