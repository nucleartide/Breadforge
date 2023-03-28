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
    private GameObject pickaxe;

    [SerializeField]
    [NotNull]
    private GameObject axe;

    [SerializeField]
    [NotNull]
    private GameObject bottle;

    [SerializeField]
    [NotNull]
    private PlayerMiningState playerMiningState;

    [SerializeField]
    [NotNull]
    private PlayerChoppingState playerChoppingState;

    [SerializeField]
    [NotNull]
    private PlayerScoopingState playerScoopingState;

    private void PlayerStateMachine_OnChanged(object sender, StateMachineBehaviour.StateMachineChangedArgs args)
    {
        Debug.Log("PlayerTool : OnChanged");
        Debug.Log(args.NewState);

        pickaxe.SetActive(args.NewState == playerMiningState);
        axe.SetActive(args.NewState == playerChoppingState);
        bottle.SetActive(args.NewState == playerScoopingState);
    }
}
