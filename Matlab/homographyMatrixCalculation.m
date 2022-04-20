%% A
% x1i = 0;
% x2i = 0;
% x3i = 0.1328;
% x4i = 0.1328;
% 
% y1i = 0;
% y2i = 0.7419;
% y3i = 0.7419;
% y4i = 0;
% 
% % square
% x1 = 0;
% x2 = 0;
% x3 = 0.1328;
% x4 = 0.1328;
% 
% y1 = 0;
% y2 = 0.7419;
% y3 = 0.7419;
% y4 = 0;

%% B
% x1i = 0;
% x2i = 0;
% x3i = 1;
% x4i = 1;
% 
% y1i = 0.7419;
% y2i = 1;
% y3i = 0.7662;
% y4i = 0.7419;
% 
% % square
% x1 = 0;
% x2 = 0;
% x3 = 1;
% x4 = 1;
% 
% y1 = 0.7419;
% y2 = 1;
% y3 = 1;
% y4 = 0.7419;
%% C
x1i = 0.1328;
x2i = 0.1328;
x3i = 1;
x4i = 1;

y1i = 0;
y2i = 0.7419;
y3i = 0.7419;
y4i = 0.7257;

% square
x1 = 0.1328;
x2 = 0.1328;
x3 = 1;
x4 = 1;

y1 = 0;
y2 = 0.7419;
y3 = 0.7419;
y4 = 0;

%% Calculations for homography matrix
P = [-x1 -y1 -1 0 0 0 x1*x1i y1*x1i x1i;
    0 0 0 -x1 -y1 -1 x1*y1i y1*y1i y1i;
    -x2 -y2 -1 0 0 0 x2*x2i y2*x2i x2i;
    0 0 0 -x2 -y2 -1 x2*y2i y2*y2i y2i;
    -x3 -y3 -1 0 0 0 x3*x3i y3*x3i x3i;
    0 0 0 -x3 -y3 -1 x3*y3i y3*y3i y3i;
    -x4 -y4 -1 0 0 0 x4*x4i y4*x4i x4i;
    0 0 0 -x4 -y4 -1 x4*y4i y4*y4i y4i;
    0 0 0 0 0 0 0 0 1; 
]

b = [zeros(length(P)-1,1); 1;];


h = P \ b
h = (reshape(h,[3,3]))'

%% use Homography
 
xirr = [0.1;0.6916;1]
xsq = h \ xirr;
xsq = xsq/xsq(3)

% xsq=[0.9;0.3458;1]
% xirr = h*xsq;
% xirr = xirr/xirr(3)



% hB
% 
%    10.6214         0         0
%     7.1381    1.0000         0
%     9.6214         0    1.0000


% hC
%    -8.9858         0    1.1706
%    -6.5399   -0.1706    0.8685
%    -8.8151         0    1.0000