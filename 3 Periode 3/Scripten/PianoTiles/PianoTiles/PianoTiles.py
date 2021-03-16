#! python3
import pyautogui, sys, keyboard
while True:
    if keyboard.is_pressed('v'):
        pyautogui.click(730, 750)
    elif keyboard.is_pressed('b'):
        pyautogui.click(880, 750)
    elif keyboard.is_pressed('n'):
        pyautogui.click(1030, 750)
    elif keyboard.is_pressed('m'):
        pyautogui.click(1180, 750)