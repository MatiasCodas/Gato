using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatHorizontal : MonoBehaviour
{
    public float distance = 5f; // Distância total que a plataforma deve percorrer
    public float speed = 2f; // Velocidade da plataforma (ajuste conforme necessário)

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float t = 0f; // Parâmetro de interpolação
    private bool movingUp = true; // Indica se a plataforma está se movendo para cima

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * distance; // Define a posição alvo com base na distância
    }

    private void Update()
    {
        t += speed * Time.deltaTime;
        if (t > 1f)
        {
            t = 0f;
            movingUp = !movingUp; // Inverte a direção quando atinge o limite
        }

        // Interpolação linear entre as posições inicial e alvo
        if (movingUp)
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
        else
            transform.position = Vector3.Lerp(targetPosition, initialPosition, t);
    }
}
