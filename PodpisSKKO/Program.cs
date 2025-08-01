using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace PodpisSKKO
{
    internal static class Program
    {
        // Статическая переменная для пути к лог-файлу
        static string currentLogFilePath;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. Выбор файла для подписи
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = "Выберите файл, который нужно подписать",
                Filter = "Все файлы (*.*)|*.*"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                LogMessage("Файл не выбран.");
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
                LogMessage("Папка не выбрана.");
                return;
            }

            string outputDirectory = folderDialog.SelectedPath;

            // 3. Создание лог-файла
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            currentLogFilePath = Path.Combine(outputDirectory, $"{timestamp}_LOG.txt");
            LogMessage("Запущено приложение для подписи.");

            string fileNameJson = Path.GetFileName(inputFile);
            string outputFile = Path.Combine(outputDirectory, $"{timestamp}_{fileNameJson}.p7s");

            // 4. Удаление существующего файла, если он есть
            if (File.Exists(outputFile))
            {
                try
                {
                    File.Delete(outputFile);
                    LogMessage($"Удалён существующий файл подписи: {outputFile}");
                }
                catch (Exception delEx)
                {
                    LogMessage("Ошибка при удалении файла подписи: " + delEx.Message);
                    return;
                }
            }

            // 5. Копирование JSON-файла, если он отсутствует
            string destinationJsonPath = Path.Combine(outputDirectory, fileNameJson);
            if (!File.Exists(destinationJsonPath))
            {
                try
                {
                    File.Copy(inputFile, destinationJsonPath, false);
                    LogMessage($"Скопирован JSON-файл: {fileNameJson}");
                }
                catch (Exception copyEx)
                {
                    LogMessage("Ошибка копирования JSON-файла: " + copyEx.Message);
                    return;
                }
            }

            // 6. Определение пути к AvCmUt4.exe
            string nPathAvest = Environment.Is64BitOperatingSystem
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"Avest\AvPCM_nces")
                : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Avest\AvPCM_nces");

            string command = Path.Combine(nPathAvest, "AvCmUt4.exe");
            string logPath = Path.Combine(outputDirectory, "AvCmUt4.log");

            // 7. Аргументы для процесса подписи
            string args = $"-s \"{inputFile}\" -T -m1 -M -LOG \"{logPath}\" -o \"{outputFile}\"";
            LogMessage("Сформированы параметры команды подписи.");

            // 8. Запуск подписи
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = command;
                process.StartInfo.Arguments = args;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                process.WaitForExit();

                LogMessage($"Файл успешно подписан: {outputFile}");
            }
            catch (Exception ex)
            {
                LogMessage("Ошибка при запуске AvCmUt4: " + ex.Message);
            }
        }

        static void LogMessage(string message)
        {
            try
            {
                File.AppendAllText(currentLogFilePath, $"{DateTime.Now:HH:mm:ss} - {message}\n");
            }
            catch
            {
                // Ошибки логирования игнорируются
            }
        }
    }
}
