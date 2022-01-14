import requests, json, turtle

screen = turtle.Screen()
screen.setup(1000,500)
screen.bgpic('earth.gif')
screen.setworldcoordinates(-180, -90, 180, 90)

iss = turtle.Turtle()
iss.shape("circle")
iss.color('red')
iss.penup()

def track_iss():
    response = requests.get(url)
    if response.status_code == 200:
        response_dictionary = json.loads(response.text)
        position = response_dictionary['iss_position']
        iss.goto(float(position['latitude']), float(position['longitude']))
        iss.pendown()
    else:
        print('Error:', response.status_code)
    widget = turtle.getcanvas()
    print(float(position['latitude']), float(position['longitude']))
    widget.after(1000, track_iss)

url = 'http://api.open-notify.org/iss-now.json'

track_iss()

turtle.mainloop()
