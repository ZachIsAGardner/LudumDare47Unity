using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Mover : MonoBehaviour
{
    /// <summary>
    /// Current velocity for this moving object.
    /// </summary>
    [HideInInspector] public Vector2 Velocity;

    /// <summary>
    /// This value is added on top of Velocity when applying translations.
    /// </summary>
    [HideInInspector] public Vector2 VelocityOffset;


    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float skin = 0.1f;
    [SerializeField] private int rayCount = 4;

    private CollisionInfo collisions;
    public CollisionInfo Collisions { get { return collisions; } }

    private OriginInfo origins;
    private HorizontalSlopeInfo horizontalSlopeInfo;
    private VerticalSlopeInfo verticalSlopeInfo;

    // ---
    // Status

    public bool AnyCollisions()
    {
        if (Collisions.Up || Collisions.Down
        || Collisions.Left || Collisions.Right)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // ---

    protected void GetRayOrigins()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        Bounds bounds = boxCollider2D.bounds;
        bounds.Expand(skin * -2);

        origins.BottomLeft = new Vector2(boxCollider2D.bounds.min.x, boxCollider2D.bounds.min.y);
        origins.BottomRight = new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.min.y);
        origins.TopLeft = new Vector2(boxCollider2D.bounds.min.x, boxCollider2D.bounds.max.y);
        origins.TopRight = new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.max.y);
    }

    // ---

    /// <summary>
    /// Moves the entity. Provided the velocity to attempt to move the entity with. 
    /// Velocity will change based on collision.
    /// </summary>
    public void Move(Vector2 vel)
    {
        // Reset
        collisions.Reset();
        horizontalSlopeInfo.Reset();
        verticalSlopeInfo.Reset();

        GetRayOrigins();

        // Adjust vel based on collisions
        HorizontalCollisions(ref vel);
        if (vel.y != 0) VerticalCollisions(ref vel);

        // Move object with velocity
        transform.Translate(vel);
    }

    /// <summary>
    /// Moves the entity while on a moving platform.
    /// </summary>
    public void Move(Vector2 vel, bool forceGrounded)
    {
        collisions.Reset();
        collisions.Down = forceGrounded;
        GetRayOrigins();
        HorizontalCollisions(ref vel);
        // if (vel.y != 0) {
        // 	VerticalCollisions(ref vel);
        // }
        transform.Translate(vel);
    }

    void HorizontalCollisions(ref Vector2 vel)
    {
        float dir = Mathf.Sign(vel.x);
        float length = Mathf.Abs(vel.x) + skin;
        float spacing = GetComponent<BoxCollider2D>().bounds.size.y / (rayCount - 1);

        if (Mathf.Abs(vel.x) < skin)
            length = 2 * skin;

        for (int i = 0; i < rayCount; i++)
        {
            Vector2 rayOrigin = (dir == -1) ? origins.BottomLeft : origins.BottomRight;
            rayOrigin += Vector2.up * (spacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * dir, length, layerMask);

            UnityEngine.Debug.DrawRay(rayOrigin, Vector2.right * dir, Color.red);

            if (hit)
            {
                if (hit.distance == 0 || hit.collider.tag == "Through")
                    continue;

                // Make sure not setting vel to 0 on left or right collision if using slope
                // check bottom raycast only
                if (i == 0)
                {
                    // HorizontalAdjustForSlopeBottom(hit, ref vel);
                }
                // check top raycast only
                else if (i == rayCount - 1)
                {
                    // HorizontalAdjustForSlopeTop(hit, ref vel);
                }

                vel.x = (hit.distance - skin) * dir;
                length = hit.distance;

                collisions.Left = dir == -1;
                collisions.Right = dir == 1;
                collisions.col = hit.collider;

            }
        }
    }

    void VerticalCollisions(ref Vector2 vel)
    {
        float dir = Mathf.Sign(vel.y);
        float length = Mathf.Abs(vel.y) + skin;
        float spacing = GetComponent<BoxCollider2D>().bounds.size.x / (rayCount - 1);

        for (int i = 0; i < rayCount; i++)
        {
            Vector2 rayOrigin = (dir == -1) ? origins.BottomLeft : origins.TopLeft;
            rayOrigin += Vector2.right * (spacing * i + vel.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * dir, length, layerMask);

            UnityEngine.Debug.DrawRay(rayOrigin, Vector2.up * dir, Color.red);

            if (hit)
            {
                if (hit.collider.tag == "Through" && vel.y >= 0)
                    continue;


                vel.y = (hit.distance - skin) * dir;
                length = hit.distance;

                collisions.Down = dir == -1;
                collisions.Up = dir == 1;
                collisions.col = hit.collider;
            }
        }
    }

    // ---
    // Structs

    public struct OriginInfo
    {
        public Vector2 BottomLeft, BottomRight, TopLeft, TopRight;
    }

    public struct CollisionInfo
    {
        public bool Up, Down, Left, Right;
        public Collider2D col;

        public void Reset()
        {
            col = null;
            Up = Down = false;
            Left = Right = false;

        }
    }

    private struct HorizontalSlopeInfo
    {
        public float SlopeAngle, SlopeAngleOld;
        public bool ClimbingSlope;

        public void Reset()
        {
            SlopeAngleOld = SlopeAngle;
            SlopeAngle = 0;

            ClimbingSlope = false;
        }
    }

    private struct VerticalSlopeInfo
    {
        public float SlopeAngle, SlopeAngleOld;
        public bool ClimbingSlope;

        public void Reset()
        {
            SlopeAngleOld = SlopeAngle;
            SlopeAngle = 0;

            ClimbingSlope = false;
        }
    }
}