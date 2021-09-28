public class MoveCommand : Command
{
    private PlayerMovement playerMovement;
    private float horizontalAxis, verticalAxis;

    public MoveCommand(PlayerMovement _playerMovement, float _horizontalAxis, float _verticalAxis)
    {
        this.playerMovement = _playerMovement;
        this.horizontalAxis = _horizontalAxis;
        this.verticalAxis = _verticalAxis;
    }
    
    // trigger perintah movement
    public override void Execute()
    {
        // panggil method Move untuk membuat karakter bergerak
        playerMovement.Move(horizontalAxis, verticalAxis);
        
        // panggil method Animating untuk memulai animasi karakter
        playerMovement.Animating(horizontalAxis, verticalAxis);
    }

    public override void Unexecute()
    {
        // invers arah dari movement player
        playerMovement.Move(- horizontalAxis, - verticalAxis);
        
        // menganimasikan player
        playerMovement.Animating(horizontalAxis, verticalAxis);
    }
}
