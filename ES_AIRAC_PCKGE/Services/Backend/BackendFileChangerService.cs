using System.IO;
using System.Text;

namespace ES_AIRAC_PCKGE.Services.Backend;

partial class BackendFileChangerService
{

    private ConfigService _configService = null;

    public Task RemoveFromFileChanger(ConfigService configService, int i)
    {
        _configService = configService;
        return Task.CompletedTask;
    }

    public Task ChangeInFileChanger(ConfigService configService, int i)
    {
        List<string> LineListString = new List<string>();
        
        //4File
        string FileRead = File.ReadAllText(configService.GetFeatureListFileName()[i]);
        
        LineListString.AddRange(File.ReadAllLines(configService.GetFeatureListFileName()[i]));



        foreach (var line in LineListString)
        {
            if (configService.GetFeatureListSpecialFeature() == null)
            {
                if (line.Contains(configService.GetFeatureListOld()[i]))
                {
                    line.Replace(configService.GetFeatureListOld()[i], configService.GetFeatureListNew()[i]);
                }
            }
            else
            {
                
            }
        }








        return Task.CompletedTask;
    }

    public Task AddFileChanger(ConfigService configService, int i)
    {
        _configService = configService;
        File.AppendAllText(configService.GetFeatureListFileName()[i], configService.GetFeatureListNew()[i]);
        return Task.CompletedTask;
    }

    public Task RemoveFileChanger(ConfigService configService, int i)
    {
        _configService = configService;
        File.Delete(configService.GetFeatureListFileName()[i]);
        return Task.CompletedTask;
    }
    
}