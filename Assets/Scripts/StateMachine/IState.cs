using UnityEngine;

// ���ߧ�֧��֧ۧ� �������ߧڧ�
public interface IState
{
    // ����٧�ӧѧ֧��� ���� �ӧ��է� �� �������ߧڧ�
    public void Enter();

    // ����٧�ӧѧ֧��� ���� �ӧ���է� �ڧ� �������ߧڧ�
    public void Exit();

    // ����٧�ӧѧ֧��� �էݧ� ��ҧ�ѧҧ��ܧ� �ӧӧ�է�
    public void HandleInput();

    // ����٧�ӧѧ֧��� �էݧ� ��ҧߧ�ӧݧ֧ߧڧ� �ݧ�ԧڧܧ� �������ߧڧ�
    public void Update();

    // ����٧�ӧѧ֧��� �էݧ� ��ҧߧ�ӧݧ֧ߧڧ� ��ڧ٧ڧܧ� �������ߧڧ�
    public void PhysicsUpdate();
}
