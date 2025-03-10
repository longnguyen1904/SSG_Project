using UnityEngine;
using UnityEngine.UI;

public class MinimapController : MonoBehaviour
{
    public GameObject[] players; // Mảng chứa 3 nhân vật (Player1, Player2, Player3)
    public RectTransform minimapDot; // Gắn Image (điểm trắng) vào đây

    // Kích thước bản đồ game (thay đổi theo bản đồ của bạn)
    private float mapWidth = 1000f;
    private float mapHeight = 800f;

    // Kích thước minimap (khớp với RawImage)
    private float minimapWidth = 200f;
    private float minimapHeight = 160f;

    private float scaleX;
    private float scaleY;

    void Start()
    {
        // Tính tỷ lệ thu nhỏ
        scaleX = minimapWidth / mapWidth;
        scaleY = minimapHeight / mapHeight;
    }

    void Update()
    {
        // Tìm nhân vật đang active
        Transform activePlayer = GetActivePlayer();

        if (activePlayer != null)
        {
            // Lấy vị trí thực tế của nhân vật đang active
            Vector2 playerPos = activePlayer.position;

            // Chuyển đổi sang tọa độ trên minimap
            float minimapX = playerPos.x * scaleX;
            float minimapY = playerPos.y * scaleY;

            // Cập nhật vị trí điểm trắng
            minimapDot.anchoredPosition = new Vector2(minimapX, minimapY);
        }
    }

    // Hàm tìm nhân vật đang active
    private Transform GetActivePlayer()
    {
        foreach (GameObject player in players)
        {
            if (player.activeInHierarchy) // Kiểm tra xem nhân vật có active không
            {
                return player.transform;
            }
        }
        return null; // Nếu không có nhân vật nào active
    }
}