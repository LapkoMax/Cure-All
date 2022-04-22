import { css } from "@emotion/react";
import { gray3 } from "../Common/Colors";

export const doctorListPageContainer = css`
  height: fit-content;
  max-width: 100vw;
  overflow-x: hidden;
`;

export const doctorParametersContainer = css`
  margin: 50px auto 20px auto;
  padding: 30px 20px;
  max-width: 100vw;
  border: solid;
  border-color: ${gray3};
  border-width: 1px;
  border-radius: 20px;
  height: fit-content;
  background-color: rgba(255, 255, 255, 0.5);
`;
