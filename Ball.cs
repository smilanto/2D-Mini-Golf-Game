using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private GameObject goalFX;  // Add GoalFX GameObject here

    [Header("Attributes")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    [SerializeField] private float maxGoalSpeed = 4f;

    private bool isDragging;
    private bool inHole;

    private void Update() {
        PlayerInput();
    }

    private bool isReady() {
        // Ensure the ball is nearly stopped before allowing input
        return rb.velocity.magnitude <= 0.2f;
    }

    private void PlayerInput() {
        if (!isReady()) return;  // Only allow input if the ball is ready (not moving)

        Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get mouse position in world space
        float distance = Vector2.Distance(transform.position, inputPos); // Calculate distance between ball and mouse

        if (Input.GetMouseButtonDown(0) && distance <= 0.5f) DragStart(); // Start dragging if mouse is close enough
        if (Input.GetMouseButton(0) && isDragging) DragChange(inputPos);  // Update dragging while holding mouse button
        if (Input.GetMouseButtonUp(0) && isDragging) DragRelease(inputPos); // Release the ball when mouse button is lifted
    }

    private void DragStart() {
        isDragging = true;
        lr.positionCount = 2;  // Activate LineRenderer for showing direction
    }

    private void DragChange(Vector2 pos) {  // Take the position from PlayerInput
        Vector2 dir = (Vector2)transform.position - pos; // Calculate direction from ball to mouse
        lr.SetPosition(0, transform.position); // Set the start of the line to the ball's position
        lr.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude((dir * power) / 2, maxPower / 2));  // Set end of line
    }

    private void DragRelease(Vector2 pos) {
        float distance = Vector2.Distance((Vector2)transform.position, pos); // Distance from the release point
        isDragging = false;
        lr.positionCount = 0;  // Hide the LineRenderer

        if (distance < 1f) {
            return;  // Don't apply force if the distance is too small
        }

        Vector2 dir = (Vector2)transform.position - pos;  // Calculate the direction to apply the force
        rb.velocity = Vector2.ClampMagnitude(dir * power, maxPower);  // Apply force to the ball based on drag release
    }

    private void CheckWinState() {
        if (inHole) return;  // Don't check again if the ball is already in the hole

        if (rb.velocity.magnitude <= maxGoalSpeed) {  // Check if the ball's velocity is slow enough to consider it in the hole
            inHole = true;
            rb.velocity = Vector2.zero;  // Stop the ball's movement
            gameObject.SetActive(false);  // Deactivate the ball to simulate it entering the hole
            GameObject fx =Instantiate(goalFX,transform.position,Quaternion.identity);
            Destroy(fx,2f);
        }
    }

    // Called when the ball enters a trigger collider (like the hole)
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Goal")) {  // Check if the ball entered the goal
            CheckWinState();
        }
    }

    // Called while the ball stays inside a trigger collider (like the hole)
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Goal")) {  // Continue checking if the ball is in the goal
            CheckWinState();
        }
    }
}
