﻿using Kubeless.Core.Interfaces;
using Kubeless.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kubeless.Tests.Utils
{
    public static class FunctionCreator
    {
        private static IFunctionSettings BuildFunctionSettings(string functionFile, string moduleName, string functionHandler, string requirementsFile = "")
        {
            var functionInfo = new FileInfo(functionFile);
            var basePath = functionInfo.Directory.FullName;
            var baseName = functionInfo.Name.Replace(".cs","");

            var code = new StringContent(functionFile);
            var requirements = new StringContent(requirementsFile);
            var assembly = new BinaryContent(Path.Combine(basePath, $"{baseName}.dll"));

            return new FunctionSettings(moduleName, functionHandler, code, requirements, assembly);
        }

        public static IFunction CreateFunction(string functionFile, string requirementsFile = "", string moduleName = "module", string functionHandler = "handler")
        {
            var settings = BuildFunctionSettings(functionFile, moduleName, functionHandler, requirementsFile);
            return new Function(settings);
        }
    }
}
