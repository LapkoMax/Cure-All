import { UserData } from "../../Api/IdentityData";
import {
  loggingUserAction,
  loginedUserAction,
  signOutUserAction,
} from "../ActionCreators/IdentityActionCreators";
import {
  LOGGINGUSER,
  LOGINEDUSER,
  SIGNOUTUSER,
} from "../Actions/IdentityActions";

type IdentityActions =
  | ReturnType<typeof loggingUserAction>
  | ReturnType<typeof loginedUserAction>
  | ReturnType<typeof signOutUserAction>;

export interface IdentityState {
  readonly loading: boolean;
  readonly returnUrl: string;
  readonly token?: string;
  readonly user?: UserData | null;
}

const initialIdentityState: IdentityState = {
  loading: false,
  returnUrl: "",
  token: "",
  user: null,
};

export const identityReducer = (
  state = initialIdentityState,
  action: IdentityActions,
) => {
  switch (action.type) {
    case LOGGINGUSER: {
      return {
        ...state,
        loading: true,
        token: "",
        user: null,
      };
    }
    case LOGINEDUSER: {
      return {
        ...state,
        loading: false,
        token: action.token,
        user: action.user,
      };
    }
    case SIGNOUTUSER: {
      return {
        ...state,
        returnUrl: action.returnUrl,
        token: "",
        user: null,
      };
    }
  }
  return state;
};
