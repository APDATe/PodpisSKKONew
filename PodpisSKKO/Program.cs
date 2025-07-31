using System.Diagnostics;

namespace PodpisSKKO
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 1. Выбор файла для подписи
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = "Выберите файл, который нужно подписать",
                Filter = "Все файлы (*.*)|*.*"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Файл не выбран.");
                return;
            }

            string inputFile = fileDialog.FileName;

            // 2. Выбор папки для сохранения подписи
            FolderBrowserDialog folderDialog = new FolderBrowserDialog
            {
                Description = "Выберите папку для сохранения подписанного файла"
            };

            if (folderDialog.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Папка не выбрана.");
                return;
            }

            string outputDirectory = folderDialog.SelectedPath;
            string outputFile = Path.Combine(outputDirectory, Path.GetFileName(inputFile) + ".p7s");

            // 3. Определение пути к AvCmUt4.exe
            string nPathAvest;
            if (Environment.Is64BitOperatingSystem)
            {
                string program_files_x86_folder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                nPathAvest = Path.Combine(program_files_x86_folder, @"Avest\AvPCM_nces");
            }
            else
            {
                string program_files_folder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                nPathAvest = Path.Combine(program_files_folder, @"Avest\AvPCM_nces");
            }

            string command = Path.Combine(nPathAvest, "AvCmUt4.exe");

            // Получаем путь к директории с EXE
            string logPath = Path.Combine(outputDirectory, "AvCmUt4.log");

            // 4. Составление параметров (пример — подпись файла)
            // Формируем аргументы
            string args = $"-s \"{inputFile}\" -T -m1 -M -LOG \"{logPath}\" -o \"{outputFile}\"";

            // 5. Запуск процесса
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = command;
                process.StartInfo.Arguments = args;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                process.WaitForExit();

                MessageBox.Show("Файл успешно подписан:\n" + outputFile, "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при запуске AvCmUt4:\n" + ex.Message, "Ошибка");
            }
        }
    }
}
