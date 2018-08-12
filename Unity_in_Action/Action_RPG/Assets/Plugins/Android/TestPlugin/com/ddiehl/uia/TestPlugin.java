package com.ddiehl.uia;

public class TestPlugin {
	private static int number = 0;
	
	public static int getNumber() {
		number++;
		return number;
	}
	
	public static String getString(String message) {
		return message.toLowerCase();
	}
}
