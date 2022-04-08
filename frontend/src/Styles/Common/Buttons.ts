import styled from "@emotion/styled";
import {
  gray1,
  gray2,
  gray5,
  gray6,
  primary1,
  primary2,
  red1,
  red2,
} from "./Colors";
import { mainFontFamily, mainFontSize } from "./Fonts";

export const PrimaryButton = styled.button`
  background-color: ${primary2};
  border-color: ${primary2};
  border-style: solid;
  border-radius: 5px;
  font-family: ${mainFontFamily};
  font-size: ${mainFontSize};
  padding: 5px 10px;
  color: white;
  cursor: pointer;
  :hover {
    background-color: ${primary1};
  }
  :focus {
    outline-color: ${primary2};
  }
  :disabled {
    opacity: 0.5;
    cursor: not-allowed;
  }
`;

export const SecondaryButton = styled.button`
  background-color: ${gray1};
  border-color: ${gray2};
  border-style: solid;
  border-radius: 5px;
  font-family: ${mainFontFamily};
  font-size: ${mainFontSize};
  padding: 5px 10px;
  color: white;
  cursor: pointer;
  :hover {
    background-color: ${gray5};
  }
  :focus {
    outline-color: ${gray6};
  }
  :disabled {
    opacity: 0.5;
    cursor: not-allowed;
  }
`;

export const DangerButton = styled.button`
  background-color: ${red1};
  border-color: ${red2};
  border-style: solid;
  border-radius: 5px;
  font-family: ${mainFontFamily};
  font-size: ${mainFontSize};
  padding: 5px 10px;
  color: white;
  cursor: pointer;
  :hover {
    background-color: ${red2};
  }
  :focus {
    outline-color: ${gray6};
  }
  :disabled {
    opacity: 0.5;
    cursor: not-allowed;
  }
`;

export const FormButtonContainer = styled.div`
  margin: 30px 0px 0px 0px;
  padding: 20px 0px 0px 0px;
  border-top: 1px solid ${gray5};
`;
