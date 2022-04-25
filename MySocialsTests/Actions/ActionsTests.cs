using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySocials.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MySocials.Actions.Tests
{
    [TestClass()]
    public class ActionsTests
    {
        [TestMethod()]
        public void CopyAllToClipBoardTest()
        {
           Console.WriteLine(Directory.GetCurrentDirectory());
           try
           {
               var process = Process.Start(new ProcessStartInfo()
               {
                   FileName = "Powershell",
                  
                   ArgumentList = { Path.Combine(Directory.GetCurrentDirectory(), "MySocials.exe"),"select" },
                    UseShellExecute = true,
                   RedirectStandardInput = true,
                   RedirectStandardOutput = true
               });
               Console.SetIn(process.StandardOutput);
               Console.WindowWidth = 100;
               Console.WindowWidth = 100;
               Console.SetOut(process.StandardInput);
               Console.Write(ConsoleKey.Enter);
               process.WaitForExit();
            }
           catch(Exception ex)
           {
               Console.WriteLine(ex.Message);
           }

        }

        [TestMethod()]
        public void AddTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SelectTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SelectFromArgsTest()
        {
            Assert.Fail();
        }
    }


}