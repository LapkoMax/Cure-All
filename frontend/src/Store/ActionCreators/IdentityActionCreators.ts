import { AuthResult } from "../../Api/IdentityData";
import {
  LOGGINGUSER,
  LOGINEDUSER,
  SIGNOUTUSER,
} from "../Actions/IdentityActions";

export const loggingUserAction = () =>
  ({
    type: LOGGINGUSER,
  } as const);

export const loginedUserAction = (authResult: AuthResult) =>
  ({
    type: LOGINEDUSER,
    token: authResult.token,
    user: authResult.user,
  } as const);

export const signOutUserAction = () =>
  ({
    type: SIGNOUTUSER,
  } as const);
