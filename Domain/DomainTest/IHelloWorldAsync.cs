using SharedTest;

namespace DomainTest;

public interface IHelloWorldAsync
{
    public Task<HelloWorldResult> HelloWorld(string playFabId,HelloWorldRequest contextRequest);
}