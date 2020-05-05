using AutoMapper.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Logging
{
    public class CustomerLogger : ILogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfig;


        public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            loggerConfig = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string msg = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

            //EscreverTextoNoArquivo(msg);
        }

        private void EscreverTextoNoArquivo(string msg)
        {
            //string caminhoArquivoLog = @"D:\dados\Log\NicolasApi_log.txt";
            //if (File.Exists(caminhoArquivoLog))
            //    File.AppendAllText(caminhoArquivoLog, Environment.NewLine + $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}" + Environment.NewLine + msg);
            //else
            //{
            //    using (StreamWriter sw = new StreamWriter(caminhoArquivoLog, true))
            //    {

            //        try
            //        {
            //            sw.Close();
            //            File.AppendAllText(caminhoArquivoLog, Environment.NewLine + $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}" + Environment.NewLine + msg);
            //        }
            //        catch
            //        {
            //            throw;
            //        }
            //    }
            //}
            //string caminhoArquivoLog = @"c:\dados\Log\NicolasApi_log.txt";
            //using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true))
            //{
            //    try
            //    {
            //        streamWriter.WriteLine(msg);
            //        streamWriter.Close();
            //    }
            //    catch (Exception)
            //    {
            //        throw;
            //    }

            //}
        }
    }
}
