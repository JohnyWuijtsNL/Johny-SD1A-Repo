#! python3
import pyautogui, sys, keyboard
while True:
    if keyboard.is_pressed('Up'):
        pyautogui.click(730, 750)
    elif keyboard.is_pressed('Left'):
        pyautogui.click(880, 750)
    elif keyboard.is_pressed('Right'):
        pyautogui.click(1030, 750)
    elif keyboard.is_pressed('Down'):
        pyautogui.click(1180, 750)