using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Vueling.Common.Logic.Exceptionsa;

namespace Vueling.Common.Logic.Helpers
{
    public static class ExceptionMessage
    {
        public static void Show(Exception ex)
        {
            if (ex is ArgumentException)
                MessageBox.Show(Exceptions.ResourceManager.GetString("ArgumentException"));
            if (ex is ArgumentNullException)
                MessageBox.Show(Exceptions.ResourceManager.GetString("ArgumentNullException"));
            if (ex is ArgumentOutOfRangeException)
                MessageBox.Show(Exceptions.ResourceManager.GetString("ArgumentOutOfRangeException"));
            if (ex is DirectoryNotFoundException)
                MessageBox.Show(Exceptions.ResourceManager.GetString("DirectoryNotFoundException"));
            if (ex is FileNotFoundException)
                MessageBox.Show(Exceptions.ResourceManager.GetString("FileNotFoundException"));
            if (ex is FormatException)
                MessageBox.Show(Exceptions.ResourceManager.GetString("FormatException"));
            if (ex is IOException)
                MessageBox.Show(Exceptions.ResourceManager.GetString("IOException"));
            if (ex is OutOfMemoryException)
                MessageBox.Show(Exceptions.ResourceManager.GetString("OutOfMemoryException"));
            if (ex is OverflowException)
                MessageBox.Show(Exceptions.ResourceManager.GetString("OverflowException"));
            if (ex is PlatformNotSupportedException)
                MessageBox.Show(Exceptions.ResourceManager.GetString("PlatformNotSupportedException"));
            if (ex is TargetException)
                MessageBox.Show(Exceptions.ResourceManager.GetString("TargetException"));
        }
    }
}
