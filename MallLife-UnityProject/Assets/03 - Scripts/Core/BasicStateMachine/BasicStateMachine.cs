using UnityEngine;
using UnityEngine.InputSystem;

public class BasicStateMachine : MonoBehaviour
{
    private readonly FirstState _stateOne = new FirstState();
    private readonly SecondState _stateTwo = new SecondState();
    private readonly EndState _stateThree = new EndState();
    private readonly ExitState _exitState = new ExitState();

    private readonly Core.StateMachine _stateMachine  = new Core.StateMachine();
        
    private void Awake()
    {
        _stateMachine.ChangeState(_stateOne);
        _stateMachine.AddTransition(_stateOne, _stateTwo, Condition);
        _stateMachine.AddTransition(_stateTwo, _stateThree, Condition);
        _stateMachine.AddAnyTransition(_exitState, ExitCondition);
        _stateMachine.AddAnyTransition(_exitState, () => true);

    }
    private void Update()
    {
        _stateMachine.Tick(Time.deltaTime);
    }
    private bool Condition()
    {
        return Keyboard.current.spaceKey.wasReleasedThisFrame;
    }
    private bool ExitCondition()
    {
        return Keyboard.current.escapeKey.wasReleasedThisFrame;
    }

}
