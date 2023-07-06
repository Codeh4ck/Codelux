namespace Codelux.Tests.ResultType;

public class TestOkResult
{
    private static TestOkResult _instance;
    public static TestOkResult Instance => _instance ??= new(); 
    
    public static TestOkResult Create() => new();
    
    public string Message => "This is an Ok result";
    public bool Success = true;
}

public class TestFailResult
{
    private static TestFailResult _instance;
    public static TestFailResult Instance => _instance ??= new(); 
    
    public static TestFailResult Create() => new();
    
    public string Message => "This is a Fail result";
    public bool Success = false;
}