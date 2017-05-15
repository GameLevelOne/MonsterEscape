using UnityEngine;

public class JoystickDirection {

	public Vector2 direction;
	public Vector2 horizontal;
	public Vector2 vertical;

	public JoystickDirection(float x,float y) {
		direction = new Vector2 (x, y);
		horizontal = new Vector2 (x, 0f);
		vertical = new Vector2 (0f, y);
	}
	public JoystickDirection(Vector2 dir) {
		direction = dir;
		horizontal = new Vector2 (dir.x, 0f);
		vertical = new Vector2 (0f, dir.y);
	}

	public static JoystickDirection zero {
		get {
			return new JoystickDirection(Vector2.zero);
		}
	}

	public bool IsRight {
		get {
			if (direction.x > 0)
				return true;
			else
				return false;
		}
	}
	public bool IsLeft {
		get {
			if (direction.x < 0)
				return true;
			else
				return false;
		}
	}
	public bool IsUp {
		get {
			if (direction.y > 0)
				return true;
			else
				return false;
		}
	}
	public bool IsDown {
		get {
			if (direction.y < 0)
				return true;
			else
				return false;
		}
	}
}
