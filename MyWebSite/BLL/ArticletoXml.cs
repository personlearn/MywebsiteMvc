using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace MyWebSite.BLL
{
    public class ArticletoXml
    {
        public static bool toXml(MatchCollection input, string dirName, string filename, string rootname, string XEname,ref string ex)
        {
            try
            {
                XDocument xdoc;
                XElement root;
                string filePath = CreateFilePath(dirName, filename);
                try
                {
                    xdoc = XDocument.Load(filePath);
                    root = xdoc.Root;
                }
                catch
                {
                    xdoc = new XDocument();
                    root = new XElement(rootname);
                }
                foreach (Match item in input)
                {
                    XElement xe = new XElement(XEname,
                        new XElement("author", item.Groups["author"].Value.Trim()),
                        new XElement("body", item.Groups["article"].Value.Trim())
                        );
                    root.Add(xe);
                }
                try
                {
                    xdoc.Add(root);
                }
                catch
                {
                    
                }
                xdoc.Save(filePath);
                return true;
            }
            catch (Exception e)
            {
                ex = e.ToString();
                return false;
            }
        }

        public static string CreateFilePath(string dirName, string fileName)
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string dirPath = Path.Combine(appPath, dirName);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);

            }
            string filePath = Path.Combine(dirPath, fileName);
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath))
                {
                }
            }
            return filePath;
        }

        public static bool toTxt(string input, string dirName, string filename)
        {
            try
            {
                string fname = CreateFilePath(dirName, filename);
                FileInfo finfo = new FileInfo(fname);

                if (!finfo.Exists)
                {
                    FileStream fs;
                    fs = File.Create(fname);
                    fs.Close();
                    finfo = new FileInfo(fname);
                }
                using (FileStream fs = finfo.OpenWrite())
                {
                    StreamWriter w = new StreamWriter(fs);
                    w.BaseStream.Seek(0, SeekOrigin.End);
                    w.Write(input.Replace("\n", "\r\n") + "\n\r");
                    w.Write("------------------------------------\n\r");
                    w.Flush();
                    w.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}