using System.IO;
using ES_AIRAC_PCKGE.EnumsList;

namespace ES_AIRAC_PCKGE.Services;

public class LoggerService
{
    public async void OnStart()
    {
        string Logfile = GetLogFileStartString(HomeTabWindow.AppVersion);
        if (!Directory.Exists(System.IO.Directory.GetCurrentDirectory() + "\\debuging"))
        {
            Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "\\debuging");
        }
        await CreateLogFile(HomeTabWindow.LogPath, Logfile);
        LogMessage(SeverityLevelType.Info, "Logging Service Started");
    }
    
    public void LogMessage(SeverityLevelType severity, string message)
    {
        File.AppendAllText(HomeTabWindow.LogPath, Environment.NewLine + GetSeverityLevelString(severity) + message);
    }

    private string GetSeverityLevelString(SeverityLevelType severity)
    {
        string dateTime = DateTime.Now.ToString("hh:mm:ss || ");
        switch (severity)
        {
            case SeverityLevelType.Info: 
                return  "INFO:" + dateTime;
            case SeverityLevelType.Success:
                return "SUCCESS:" + dateTime;
            case SeverityLevelType.Warning:
                return "WARN:" + dateTime;
            case SeverityLevelType.Error:
                return "ERROR:" + dateTime;
        }
        return "";
    }

    public Task CreateLogFile(String path, String logfile = "")
    {
        File.WriteAllText(HomeTabWindow.LogPath,logfile);
        return Task.CompletedTask;
    }

    private string GetLogFileStartString(string logversion)
    {
        
        string first = "START:" + DateTime.Now.ToString("HH:mm:ss") + " || Log ES_AIRAC_PCKGE log.txt";
        string second = "START:" + DateTime.Now.ToString("HH:mm:ss") + " || Author: Julscha - Version " + logversion;
        
        
        return first + Environment.NewLine + second;
    }


}