import { css } from "@emotion/react";
import { accent1, gray4 } from "../Common/Colors";

export const notificationContainer = css`
  padding: 10px;
  border: solid;
  border-width: 2px;
  border-color: ${gray4};
  border-radius: 10px;
`;

export const notificationDesctiption = css`
  font-size: 20px;
`;

export const notificationAppointment = css`
  border-top: solid;
  border-color: ${accent1};
`;
