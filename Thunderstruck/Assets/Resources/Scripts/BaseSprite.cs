using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSprite : MonoBehaviour {
    static float xBound;
    static float yBound;
    private Vector3 spriteSize;

    // Use this for initialization
    public void BaseStart () {
        spriteSize = GetComponent<Collider2D>().bounds.size;
    }
	public static void SetBounds(float xBound, float yBound)
    {
        BaseSprite.xBound = xBound;
        BaseSprite.yBound = yBound;
    }

	// Update is called once per frame
	public void BaseUpdate() {

        
        if (transform.position.x - (spriteSize.x/2) < MainScript.currentRoomX + -1 * xBound)
        {
            transform.position = new Vector3(
                MainScript.currentRoomX + (-1 * xBound) + (spriteSize.x / 2),
                transform.position.y,
                transform.position.z
                );
        }
        else if (transform.position.x + (spriteSize.x / 2) > MainScript.currentRoomX + (xBound))
        {
            transform.position = new Vector3(
                MainScript.currentRoomX + (xBound) - (spriteSize.x / 2),
                transform.position.y,
                transform.position.z
                );
        }

        // Y axis
        if (transform.position.y - (spriteSize.y / 2) < MainScript.currentRoomY + (-1 * yBound))
        {
            transform.position = new Vector3(
                transform.position.x, 
                MainScript.currentRoomY + (-1 * yBound) + (spriteSize.y / 2),
                transform.position.z
                );
        }
        else if (transform.position.y + (spriteSize.y / 2) > MainScript.currentRoomY + ((float)yBound))
        {
            transform.position = new Vector3(
                transform.position.x, 
                MainScript.currentRoomY + ((float)yBound) - (spriteSize.y / 2),
                transform.position.z
                );
        }
    }

    //destroy object if it runs out of bounds (instead of correcting its position)
    public void BaseUpdate_DestroyOnBoundsCheck(GameObject obj)
    {
        if (transform.position.x - (spriteSize.x / 2) < MainScript.currentRoomX + -1 * xBound)
        {
            Destroy(obj);
        }
        else if (transform.position.x + (spriteSize.x / 2) > MainScript.currentRoomX + (xBound))
        {
            Destroy(obj);
        }

        // Y axis
        if (transform.position.y - (spriteSize.y / 2) < MainScript.currentRoomY + (-1 * yBound))
        {
            Destroy(obj);
        }
        else if (transform.position.y + (spriteSize.y / 2) > MainScript.currentRoomY + ((float)yBound))
        {
            Destroy(obj);
        }
    }

    public void BaseUpdate_ReflectOnBoundsCheck(Rigidbody2D rb)
    {
        Vector2 yNorm = new Vector2(0, 1);
        Vector2 xNorm = new Vector2(1, 0);

        if (transform.position.x - (spriteSize.x / 2) < MainScript.currentRoomX + -1 * xBound)
        {
            rb.velocity = Vector2.Reflect(rb.velocity, xNorm);
        }
        else if (transform.position.x + (spriteSize.x / 2) > MainScript.currentRoomX + (xBound))
        {
            rb.velocity = Vector2.Reflect(rb.velocity, xNorm);
        }

        // Y axis
        if (transform.position.y - (spriteSize.y / 2) < MainScript.currentRoomY + (-1 * yBound))
        {
            rb.velocity = Vector2.Reflect(rb.velocity, yNorm);
        }
        else if (transform.position.y + (spriteSize.y / 2) > MainScript.currentRoomY + ((float)yBound))
        {
            rb.velocity = Vector2.Reflect(rb.velocity, yNorm);
        }
        
    }

    public void Reflect(Collision2D collision)
    {
        Vector2 inDirection = collision.otherRigidbody.velocity;//GetComponent<Rigidbody2D>().velocity;
        Vector2 inNormal = collision.contacts[0].normal;
        Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal);
        //GetComponent<Collider2D>().attachedRigidbody.velocity = newVelocity;
        collision.otherRigidbody.velocity = newVelocity;
        //if (collision.gameObject.tag == "playerBullet")
        //{
        //    collision.rigidbody.velocity = newVelocity;
        //}
    }

    public void ReflectOther(Collision2D collision)
    {
        Vector2 inDirection = collision.rigidbody.velocity;//GetComponent<Rigidbody2D>().velocity;
        ContactPoint2D[] contacts = new ContactPoint2D[10];
        collision.otherCollider.GetContacts(contacts);
        Vector2 inNormal = contacts[0].normal;
        //inNormal = inNormal + new Vector2(Random.value / 2, Random.value / 2);
        Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal);
        //GetComponent<Collider2D>().attachedRigidbody.velocity = newVelocity;
        collision.rigidbody.velocity =  newVelocity * 2;
        //if (collision.gameObject.tag == "playerBullet")
        //{
        //    collision.rigidbody.velocity = newVelocity;
        //}
    }

    public IEnumerator BlinkGameObject(GameObject Target, int blinks, float blinkRate)
    {
        Component[] a = Target.GetComponentsInChildren(typeof(Renderer));

        for (int i = 0; i < blinks * 2; i++)
        {
            foreach (Component b in a)
            {
                Renderer c = (Renderer)b;
                c.enabled = !c.enabled;
            }
            yield return new WaitForSeconds(blinkRate);

        }
    }

}
