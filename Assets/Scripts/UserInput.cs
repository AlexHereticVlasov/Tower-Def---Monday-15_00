using Speed;
using SpellSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;

public abstract class UserInputBase : MonoBehaviour
{
    private void Update() => ReadInput();

    protected abstract void ReadInput(); 
}


public class UserInput : UserInputBase, IUserInput
{
    [Inject] private readonly ISelection _selection;
    [SerializeField] private Pause _pause;
    [SerializeField] private GameSpeed _gameSpeed;
    [SerializeField] private SpellMediator _spellMediator;

    public IInputState Current { get; private set; }

    public IPause Pause => _pause;
    public IGameSpeed GameSpeed => _gameSpeed;

    public ISpellMediator SpellMediator => _spellMediator;

    public ISelection Selection => _selection;

    private void Start()
    {
        SetState(new InputStateSelection(this));

        SpellMediator.Finished += () => SetState(new InputStateSelection(this));
        SpellMediator.Started += () => SetState(new InputStateCast(this));
    }


    public void SetState(IInputState newState)
    {
        Current?.Deactivate();
        Current = newState;
    }

    protected override void ReadInput() => Current.HandleInput();
}

public interface IUserInput
{
    public IInputState Current { get; }

    public IPause Pause { get; }
    public IGameSpeed GameSpeed { get; }
    public ISpellMediator SpellMediator { get; }
    public ISelection Selection { get; }

    void SetState(IInputState newState);
}

public interface IPause 
{
    void SetOnPause();
}

public interface IGameSpeed 
{
    float Value { get; }

    void SetValue(float newScale);
}

public interface ISpellMediator 
{
    public event UnityAction Started;
    public event UnityAction Finished;

    public bool HasCast { get; }

    public bool TryGetPointUnderCursor(out Vector3 point);

    public void Cast(Vector3 point);

    public void Cancel();
}

public interface IInputState
{
    IUserInput Context { get; } 

    void HandleInput();
    void Deactivate();
}

public interface ISelectable
{
    event UnityAction Selected;
    event UnityAction Deselected;
    event UnityAction ValuesChanged;

    void Select();
    void Deselect();
}

public abstract class InputStateBase : IInputState
{
    public InputStateBase(IUserInput userInput)
    {
        Context = userInput;
    }

    public IUserInput Context { get; }

    public abstract void Deactivate();
    
    public virtual void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)) Context.GameSpeed.SetValue(0);
        if (Input.GetKeyDown(KeyCode.Alpha1)) Context.GameSpeed.SetValue(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Context.GameSpeed.SetValue(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Context.GameSpeed.SetValue(3);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Context.Pause.SetOnPause();
        }
    }
}

public class InputStateSelection : InputStateBase
{
    public InputStateSelection(IUserInput userInput) : base(userInput)
    {
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Context.Selection.GetSelectableUnderPointer();
        }

        if (Input.GetMouseButtonDown(1))
        {
            //ToDo: Close Panel
        }
    }

    public override void Deactivate()
    {
        //ToDo: Close Panel
    }
}

public class InputStateCast : InputStateBase
{
    public InputStateCast(IUserInput userInput) : base(userInput)
    {
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (Input.GetMouseButtonDown(0))
        {
            if (Context.SpellMediator.HasCast)
            {
                if (Context.SpellMediator.TryGetPointUnderCursor(out Vector3 point))
                {
                    Context.SpellMediator.Cast(point);
                    return;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Context.SpellMediator.Cancel();
        }
    }

    public override void Deactivate()
    {
        //Context.SetState(new InputStateSelection(Context));
    }
}
