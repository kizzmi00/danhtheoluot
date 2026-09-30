using UnityEngine;
using System;

public class BattleManager : MonoBehaviour
{
    private Character player;
    private Character monster;

    private void Start()
    {
        try
        {
            // Khởi tạo người chơi và quái vật
            player = new Character("Người chơi", 100, 20);
            monster = new Character("Quái vật", 80, 15);

            // Bắt đầu vòng lặp trận đấu (Tách riêng hàm, không để trong Start)
            StartBattleLoop();
        }
        catch (Exception ex)
        {
            Debug.LogError($"[LỖI KHỞI TẠO]: {ex.Message}");
        }
    }

    // Vòng lặp trận đấu riêng biệt
    private void StartBattleLoop()
    {
        int turnCount = 1;

        while (player.IsAlive && monster.IsAlive)
        {
            Debug.Log($"--- LƯỢT {turnCount} ---");

            // 1. Lượt của người chơi (Thực hiện hành động ngẫu nhiên đại diện cho lựa chọn 1, 2, 3)
            // Trong game thực tế sẽ bấm nút/nhập số, ở đây tự chọn số 1 (Tấn công), 2 (Hồi máu), 3 (Phòng thủ)
            int playerChoice = UnityEngine.Random.Range(1, 4);
            ExecutePlayerTurn(playerChoice);

            // Kiểm tra xem quái vật đã bị tiêu diệt chưa
            if (!monster.IsAlive)
            {
                Debug.Log(">>> QUÁI VẬT ĐÃ BỊ HẠ GỤC! NGƯỜI CHƠI CHIẾN THẮNG! <<<");
                break;
            }

            // 2. Lượt của quái vật (Quái vật tự tấn công hoặc phòng thủ)
            ExecuteMonsterTurn();

            // Kiểm tra xem người chơi đã chết chưa
            if (!player.IsAlive)
            {
                Debug.Log(">>> NGƯỜI CHƠI ĐÃ THẤT BẠI! GAME OVER! <<<");
                break;
            }

            turnCount++;
        }

        Debug.Log("=== TRẬN ĐẤU KẾT THÚC ===");
    }

    // Xử lý hành động người chơi dựa trên lựa chọn số
    private void ExecutePlayerTurn(int actionChoice)
    {
        try
        {
            // Reset trạng thái phòng thủ lượt trước
            player.SetDefending(false);

            switch (actionChoice)
            {
                case 1:
                    Debug.Log("Người chơi chọn: [1. Tấn công]");
                    player.Attack(monster);
                    break;
                case 2:
                    Debug.Log("Người chơi chọn: [2. Hồi máu]");
                    player.Heal(15);
                    break;
                case 3:
                    Debug.Log("Người chơi chọn: [3. Phòng thủ]");
                    player.SetDefending(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(actionChoice), "Lựa chọn hành động phải là 1, 2 hoặc 3!");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[LỖI LƯỢT NGƯỜI CHƠI]: {ex.Message}");
        }
    }

    // Xử lý lượt quái vật
    private void ExecuteMonsterTurn()
    {
        try
        {
            monster.SetDefending(false);

            // Quái vật ngẫu nhiên tấn công (80%) hoặc phòng thủ (20%)
            int monsterChoice = UnityEngine.Random.Range(1, 11);
            if (monsterChoice <= 8)
            {
                Debug.Log("Quái vật hành động: Tấn công!");
                monster.Attack(player);
            }
            else
            {
                Debug.Log("Quái vật hành động: Phòng thủ!");
                monster.SetDefending(true);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[LỖI LƯỢT QUÁI VẬT]: {ex.Message}");
        }
    }
}