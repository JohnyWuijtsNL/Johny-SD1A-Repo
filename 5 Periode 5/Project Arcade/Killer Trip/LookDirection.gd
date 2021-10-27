extends Position2D
var extention = 5
var bullet = preload("res://TestBullet.tscn")
var bullet_speed = 150
var fire_rate = 0.2
var can_fire = true
var fired = false
var shoot_direction = Vector2.ZERO
var idle_position = Vector2.RIGHT

func _process(delta):

	shoot_direction = Vector2.ZERO
	fired = false
	if Input.is_action_pressed("shoot_up"):
		shoot_direction.y -= extention
		fired = true
	if Input.is_action_pressed("shoot_down"):
		shoot_direction.y += extention
		fired = true
	if Input.is_action_pressed("shoot_left"):
		shoot_direction.x -= extention
		fired = true
	if Input.is_action_pressed("shoot_right"):
		shoot_direction.x += extention
		fired = true
	
	if not fired:
		shoot_direction = idle_position
	position = shoot_direction
	idle_position = shoot_direction
	
	if fired and can_fire:
		var bullet_instance = bullet.instance()
		bullet_instance.position = get_parent().get_node("PlayerSprite/BulletPoint").get_global_position()
		bullet_instance.rotation_degrees = get_parent().get_node("PlayerSprite").rotation_degrees
		bullet_instance.apply_impulse(Vector2(), Vector2(bullet_speed, 0).rotated(get_parent().get_node("PlayerSprite").rotation))
		get_parent().get_parent().add_child(bullet_instance)
		can_fire = false
		yield(get_tree().create_timer(fire_rate), "timeout")
		can_fire = true
