export interface NotificationData {
  id: string;
  userId: string;
  appointmentId: string;
  readed: boolean;
  message: string;
}

export interface ResponseNotifications {
  data: NotificationData[];
  status: number;
}

export interface ResponseNotification {
  data: NotificationData | null;
  status: number;
}

const getHeaders = (token?: string): Headers => {
  let headers = new Headers();

  headers.append("Content-Type", "application/json");
  headers.append("Accept", "application/json");
  headers.append("Access-Control-Allow-Origin", "http://localhost:5000");
  headers.append("Access-Control-Allow-Headers", "true");
  headers.append("Origin", "http://localhost:5000");
  headers.append("Authorization", "bearer " + token);

  return headers;
};

export const getUserUnreadNotificationsAmount = async (
  userId?: string,
  token?: string,
): Promise<number> => {
  var headers = getHeaders(token);

  const response = await fetch(
    "http://localhost:5000/api/notifications/" + userId + "/unreaded",
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status !== 200) return 0;

  var result = await response.json();

  return result;
};

export const getUserNotifications = async (
  userId?: string,
  token?: string,
): Promise<ResponseNotifications> => {
  var headers = getHeaders(token);

  const response = await fetch(
    "http://localhost:5000/api/notifications/forUser/" + userId,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status !== 200) return { data: [], status: response.status };

  var result = await response.json();

  return { data: result, status: 200 };
};

export const getNotification = async (
  notificationId?: string,
  token?: string,
): Promise<ResponseNotification> => {
  var headers = getHeaders(token);

  const response = await fetch(
    "http://localhost:5000/api/notifications/" + notificationId,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status !== 200) return { data: null, status: response.status };

  var result = await response.json();

  return { data: result, status: 200 };
};

export const confirmNotification = async (
  notificationId?: string,
  token?: string,
): Promise<boolean> => {
  var headers = getHeaders(token);

  const response = await fetch(
    "http://localhost:5000/api/notifications/confirmNotification/" +
      notificationId,
    {
      mode: "cors",
      method: "POST",
      headers: headers,
    },
  );

  if (response.status !== 200) return false;
  return true;
};

export const rejectNotification = async (
  notificationId?: string,
  token?: string,
): Promise<boolean> => {
  var headers = getHeaders(token);

  const response = await fetch(
    "http://localhost:5000/api/notifications/rejectNotification/" +
      notificationId,
    {
      mode: "cors",
      method: "POST",
      headers: headers,
    },
  );

  if (response.status !== 200) return false;
  return true;
};

export const deleteNotification = async (
  notificationId?: string,
  token?: string,
): Promise<boolean> => {
  var headers = getHeaders(token);

  const response = await fetch(
    "http://localhost:5000/api/notifications/" + notificationId,
    {
      mode: "cors",
      method: "DELETE",
      headers: headers,
    },
  );

  if (response.status !== 204) return false;
  return true;
};
