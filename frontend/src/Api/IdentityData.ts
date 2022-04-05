export interface UserData {
  id: string;
  userName: string;
  email: string;
  type: string;
}

export interface LoginUserForm {
  login: string;
  password: string;
}

export interface AuthResult {
  success: boolean;
  errors?: string[];
  user?: UserData;
  token?: string;
}

const getHeaders = (): Headers => {
  let headers = new Headers();

  headers.append("Content-Type", "application/json");
  headers.append("Accept", "application/json");
  headers.append("Access-Control-Allow-Origin", "http://localhost:5000");
  headers.append("Access-Control-Allow-Headers", "true");
  headers.append("Origin", "http://localhost:5000");

  return headers;
};

export const loginUser = async (user: LoginUserForm): Promise<AuthResult> => {
  let headers = getHeaders();

  const response = await fetch("http://localhost:5000/login", {
    mode: "cors",
    method: "POST",
    body: JSON.stringify(user),
    headers: headers,
  });

  let result = await response.json();

  if (result.errors !== undefined)
    return { success: false, errors: result.errors };

  headers.append("Authorization", "bearer " + result.token);

  const userResponse = await fetch(
    "http://localhost:5000/userByLogin?userLogin=" + user.login,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  let userResult = await userResponse.json();

  if (userResult.errors !== undefined)
    return { success: false, errors: userResult.errors };

  return { success: true, token: result.token, user: userResult };
};
