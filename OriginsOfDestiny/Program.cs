using OriginsOfDestiny.Common.Managers;

var testService = new DIContainerManager().GetTestService();

testService.Run();