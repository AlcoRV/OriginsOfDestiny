using OriginsOfDestiny.Common.Locators;

var testService = new DIContainerLocator().GetTestService();

testService.Run();