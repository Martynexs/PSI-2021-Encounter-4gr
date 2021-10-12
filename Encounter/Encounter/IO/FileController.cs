namespace Encounter
{
    public static class FileController
    {
        public static string SelectFile()
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "CSV files(*.csv)|*.csv";

            return fileDialog.ShowDialog() == true ? fileDialog.FileName : null;
        }

        public static string CreateFile()
        {
            Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog();
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "CSV files(*.csv)|*.csv";

            return fileDialog.ShowDialog() == true ? fileDialog.FileName : null;
        }

    }
}
