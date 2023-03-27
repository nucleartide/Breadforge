using UnityEngine;

/// <summary>
/// Visually updates the player's held tool depending on the current state of the player.
/// </summary>
public class PlayerTool : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private PlayerStateMachine playerStateMachine;

    private void OnEnable() => playerStateMachine.OnChanged += PlayerStateMachine_OnChanged;

    private void OnDisable() => playerStateMachine.OnChanged -= PlayerStateMachine_OnChanged;

    [SerializeField]
    [NotNull]
    private GameObject Pickaxe;

    [SerializeField]
    [NotNull]
    private PlayerCollectingState PlayerCollectingState;

    private void PlayerStateMachine_OnChanged(object sender, StateMachineBehaviour.StateMachineChangedArgs args)
    {
        Debug.Log("PlayerTool : OnChanged");
        Debug.Log(args.NewState);
        Debug.Log(PlayerCollectingState);
        Pickaxe.SetActive(args.NewState == PlayerCollectingState);
    }
}
