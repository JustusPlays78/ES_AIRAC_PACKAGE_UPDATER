using ES_AIRAC_PCKGE.EnumsList;

namespace ES_AIRAC_PCKGE.Services.Backend;

public class BackendService
{
    private LoggerService _loggerService = new();
    private ConfigService _configService = new();
    private BackendFileChangerService _backendFileChangerService = new();

    private string configPath;

    public async void OnStart(ConfigService _configService)
    {
        _loggerService.LogMessage(SeverityLevelType.Info, "Backend Service Started");
    }

    public async void FeatureStart(string confgPath)
    {
        _configService = _configService.GetConfig();
        for(int i = 0; i < _configService.GetFeatureListBoolean().Count; i++)
        {
            if (_configService.GetFeatureListBoolean()[i])
            {
                switch (_configService.GetFeatureListTypes()[i])
                {
                    case FeaturesType.AddFile: await _backendFileChangerService.AddFileChanger(_configService, i); break;
                    case FeaturesType.RemoveFile: await _backendFileChangerService.RemoveFileChanger(_configService,i); break;
                    case FeaturesType.AddToFile: await _backendFileChangerService.AddFileChanger(_configService,i); break;
                    case FeaturesType.RemoveFromFile: await _backendFileChangerService.RemoveFileChanger(_configService,i); break;
                    case FeaturesType.ChangeInFile: await _backendFileChangerService.ChangeInFileChanger(_configService,i); break;
                }
                
            }
        }
        
    }
}