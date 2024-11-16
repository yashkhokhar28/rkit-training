export class Cookies {
  static GetCookie({ UserNameKey, PasswordKey }) {
    if (!UserNameKey || !PasswordKey) {
      throw new Error("Both UserNameKey and PasswordKey are required.");
    }

    let cookieAll = document.cookie.split(";");
    let username = null;
    let password = null;

    cookieAll.forEach((cookie) => {
      let c = cookie.trim();
      if (c.startsWith(`${UserNameKey}=`)) {
        username = c.substring(`${UserNameKey}=`.length);
      } else if (c.startsWith(`${PasswordKey}=`)) {
        password = c.substring(`${PasswordKey}=`.length);
      }
    });

    return { username, password };
  }

  static CheckCookie = () => {
    let { username, password } = Cookies.GetCookie({
      UserNameKey: "username",
      PasswordKey: "password",
    });

    if (username && password) {
      window.location.href = "sign-in.html";
    } else {
      window.location.href = "sign-up.html";
    }
  };
}
