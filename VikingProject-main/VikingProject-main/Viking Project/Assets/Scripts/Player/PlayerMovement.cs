using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private PlayerController playerController;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 7f;

    private Vector3 moveDir;
    private Vector2 lookDir;
    private bool isSprinting;

    public void UpdateMovementData(Vector3 newMovementDirection, Vector2 lookDirection) {
        moveDir = newMovementDirection;
        lookDir = lookDirection;
    }

    private void Update() {
        HandleSprintInput();   // 🟢 Tarkistetaan sprinttaus
        MovePlayer();
       
        if (playerController.isFightMode && lookDir != Vector2.zero) {
            TurnPlayerFightMode();
        } else {
            TurnPlayer();
        }
    }

    private void HandleSprintInput() {
        // Tarkistaa, onko Shift painettuna
        if (Input.GetKey(KeyCode.LeftShift)) {
            isSprinting = true;
            playerController.currentMoveSpeed = sprintSpeed;
        } else {
            isSprinting = false;
            playerController.currentMoveSpeed = walkSpeed;
        }
    }

    public void MovePlayer() {
        if (!playerController.isBlocking) {
            float moveDistance = playerController.currentMoveSpeed * Time.deltaTime;
            transform.Translate(moveDir * moveDistance, Space.World);
        }
    }

    // Kääntyy liikesuunnan mukaan
    public void TurnPlayer() {
        float rotationSpeed = 10f;
        if (moveDir.sqrMagnitude > 0.01f) {
            Quaternion targetRotation = Quaternion.LookRotation(-moveDir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    // Taistelutilassa katsomissuunnan mukaan
    public void TurnPlayerFightMode() {
        float rotationSpeed = 5f;
        Vector3 lookRotation = new Vector3(lookDir.x, 0f, lookDir.y);
        if (lookDir.sqrMagnitude > 0.01f) {
            Quaternion targetRotation = Quaternion.LookRotation(-lookRotation, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}