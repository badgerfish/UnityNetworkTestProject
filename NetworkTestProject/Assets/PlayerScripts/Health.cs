using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class Health : NetworkBehaviour {

	public const int maxHealth = 100;

	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;

	public RectTransform healthBar;

	public void TakeDamage(int amount){
		if (!isServer)
		{
			return;
		}
		currentHealth -= amount;
		if (currentHealth <= 0) {
			currentHealth = maxHealth;

			//called on server, but invoked on clients
			RpcRespawn();
		}
			
	}
		
	void OnChangeHealth (int health){
		healthBar.sizeDelta = new Vector2 (health, healthBar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawn(){
		if (isLocalPlayer) {
			//spawn back at 0
			transform.position = Vector3.zero;
		}
	}

}
