using UnityEngine;
using System;

public class Character
{
    // Properties (Đóng gói, không cho sửa trực tiếp máu từ bên ngoài)
    public string Name { get; private set; }
    public int MaxHP { get; private set; }
    public int CurrentHP { get; private set; }
    public int BaseDamage { get; private set; }
    public bool IsDefending { get; private set; }

    // Kiểm tra nhân vật còn sống hay không
    public bool IsAlive => CurrentHP > 0;

    // Constructor để khởi tạo nhân vật
    public Character(string name, int maxHP, int baseDamage)
    {
        if (maxHP <= 0 || baseDamage <= 0)
        {
            throw new ArgumentException("Máu tối đa và Sát thương gốc phải lớn hơn 0!");
        }

        Name = name;
        MaxHP = maxHP;
        CurrentHP = maxHP;
        BaseDamage = baseDamage;
        IsDefending = false;
    }

    // Tấn công đối thủ
    public void Attack(Character target)
    {
        if (target == null)
        {
            throw new ArgumentNullException(nameof(target), "Mục tiêu tấn công không tồn tại!");
        }

        // Tín toán sát thương ngẫu nhiên trong khoảng 80% đến 120%
        float multiplier = UnityEngine.Random.Range(0.8f, 1.2f);
        int finalDamage = Mathf.RoundToInt(BaseDamage * multiplier);

        Debug.Log($"{Name} tấn công {target.Name} với {finalDamage} sát thương gốc!");
        target.TakeDamage(finalDamage);
    }

    // Nhận sát thương
    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage), "Sát thương không được âm!");
        }

        // Nếu đang phòng thủ thì giảm 50% sát thương
        if (IsDefending)
        {
            damage /= 2;
            Debug.Log($"{Name} đang phòng thủ nên giảm một nửa sát thương!");
        }

        CurrentHP -= damage;
        if (CurrentHP < 0) CurrentHP = 0;

        Debug.Log($"{Name} nhận {damage} sát thương. Máu còn lại: {CurrentHP}/{MaxHP}");
    }

    // Hồi máu
    public void Heal(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Lượng máu hồi phải lớn hơn 0!");
        }

        CurrentHP += amount;
        if (CurrentHP > MaxHP) CurrentHP = MaxHP;

        Debug.Log($"{Name} đã hồi {amount} máu. Máu hiện tại: {CurrentHP}/{MaxHP}");
    }

    // Bật trạng thái phòng thủ
    public void SetDefending(bool status)
    {
        IsDefending = status;
        if (status)
        {
            Debug.Log($"{Name} chuẩn bị phòng thủ ở lượt này!");
        }
    }
}