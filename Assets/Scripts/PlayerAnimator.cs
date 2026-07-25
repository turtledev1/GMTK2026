using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    private const string MOVE_X = "MoveX";
    private const string MOVE_Y = "MoveY";
    private const string SPEED = "Speed";

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastMovement;

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        Vector2 movement = Player.Instance.GetMovement();

        if (movement != Vector2.zero) {
            lastMovement = movement;
        }

        animator.SetFloat(MOVE_X, lastMovement.x);
        animator.SetFloat(MOVE_Y, lastMovement.y);
        animator.SetFloat(SPEED, movement.sqrMagnitude);

        if (movement.x != 0) {
            spriteRenderer.flipX = movement.x > 0;
        }
    }
}
