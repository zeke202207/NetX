using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.ModuleManager.Domain.Core.T4
{
    internal static class Extentions
    {
        /// <summary>
        /// 模板内容写入文件
        /// </summary>
        /// <param name="content">要写入的内容</param>
        /// <param name="directory">存放目录</param>
        /// <param name="fileName">文件名（带扩展名)->可能包含目录</param>
        public static async Task<bool> WriteToFile(this string content, string directory, string fileName)
        {
            CreateDirectory(directory,fileName);
            var filePath = Path.Combine(directory, fileName);
            await File.WriteAllTextAsync(filePath, content);
            return true;
        }

        private static void CreateDirectory(string directory, string fileName)
        {
            var file = Path.Combine(directory, fileName);
            var dir = Path.GetDirectoryName(file);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
    }
}
