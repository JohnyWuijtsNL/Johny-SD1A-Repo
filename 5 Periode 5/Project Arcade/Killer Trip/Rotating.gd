extends Sprite

var rotation_speed = 1440

func _process(delta):
	rotation_degrees += rotation_speed * delta
