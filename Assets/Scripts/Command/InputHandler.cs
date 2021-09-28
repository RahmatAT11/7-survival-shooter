using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerShooting playerShooting;
    
    // Queue untuk menyimpan list command
    private Queue<Command> commands = new Queue<Command>();

    private void FixedUpdate()
    {
        // menghandle input movement
        Command moveCommand = InputMovementHandling();
        if (moveCommand != null)
        {
            commands.Enqueue(moveCommand);
            
            moveCommand.Execute();
        }
    }

    private void Update()
    {
        // menghandle shoot
        Command shootCommand = InputShootHandling();
        if (shootCommand != null)
        {
            shootCommand.Execute();
        }
    }

    Command InputMovementHandling()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");
        
        // cek jike movement sesuai dengan keynya
        if (Input.GetKey(KeyCode.D))
        {
            return new MoveCommand(playerMovement, horizontalAxis, verticalAxis);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            return new MoveCommand(playerMovement, horizontalAxis, verticalAxis);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            return new MoveCommand(playerMovement, horizontalAxis, verticalAxis);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            return new MoveCommand(playerMovement, horizontalAxis, verticalAxis);
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            // Undo movement
            return Undo();
        }
        else
        {
            return new MoveCommand(playerMovement, 0, 0);
        }
    }
    
    Command Undo()
    {
        // Jika Queue command tidak kosong, lakukan perintah undo
        if (commands.Count > 0)
        {
            Command undoCommand = commands.Dequeue();
            undoCommand.Unexecute();
        }

        return null;
    }

    Command InputShootHandling()
    {
        // jika menembak, trigger shoot command
        if (Input.GetButton("Fire1") && playerShooting.timer >= playerShooting.timeBetweenBullets)
        {
            return new ShootCommand(playerShooting);
        }
        else
        {
            return null;
        }
    }
}
