import styled from "@emotion/styled";
import { primary1, primary2 } from "./Colors";
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
