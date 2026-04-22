using UnityEngine;

public abstract class BasicState : Core.IState
{
    protected float _elapsedTime = 0;
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void Tick(float deltaTime);
}

public class FirstState : BasicState
{
    public override void OnEnter()
    {
        _elapsedTime = 0;
        Debug.Log("Starting 1st state");
    }
    public override void OnExit() => Debug.Log("Stopping 1st state");
    public override void Tick(float deltaTime)
    {
        _elapsedTime += deltaTime;
        Debug.Log($"Ticking 1st State ..... {_elapsedTime}");
    }
}
public class SecondState : BasicState
{
    private int _counter; 
    public override void OnEnter()
    {
        _counter = 0;
        Debug.Log("Starting 2nd state");
    }
    public override void OnExit() => Debug.Log("Stopping 2nd state");
    public override void Tick(float deltaTime)
    {
        _elapsedTime += deltaTime;
        Debug.Log($"Ticking 2nd State..... {_elapsedTime}, {_counter++}");
    }
}
public class EndState : BasicState
{
    public override void OnEnter()
    {
        _elapsedTime = 5;
        Debug.Log("Starting End state");
    }
    public override void OnExit() => Debug.Log("Stopping End state");
    public override void Tick(float deltaTime)
    {
        _elapsedTime += deltaTime;
        Debug.Log($"Ticking III ..... {_elapsedTime}");
    }
}
public class ExitState : BasicState
{
    public override void OnEnter() => Debug.Log("Entering exit state");
    public override void OnExit() => Debug.Log("Exiting exit state");
    public override void Tick(float deltaTime)
    {
        _elapsedTime += deltaTime;
        Debug.Log($"Exiting since ..... {_elapsedTime}");
    }
}
