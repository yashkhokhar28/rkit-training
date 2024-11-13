export class GetCookies {
  static GetCookie({ UserNameKey, PasswordKey }) {
    if (!UserNameKey || !PasswordKey) {
      throw new Error("Both UserNameKey and PasswordKey are required.");
    }

    let cookieAll = document.cookie.split(";");
    let username = "";
    let password = "";

    for (let i = 0; i < cookieAll.length; i++) {
      let c = cookieAll[i].trim();

      if (c.indexOf(UserNameKey + "=") === 0) {
        username = c.substring((UserNameKey + "=").length, c.length);
      }

      if (c.indexOf(PasswordKey + "=") === 0) {
        password = c.substring((PasswordKey + "=").length, c.length);
      }
    }

    return {
      username: username ? username : null,
      password: password ? password : null,
    };
  }
}
